import Axios from 'axios';
import React, {useEffect, useState} from 'react'
import { useDrag } from 'react-dnd'
import DropdownStyle from '../css/dropdown.module.css'

import { useMsal } from '@azure/msal-react';


/**
 * This component is responsible for displaying the appropriate
 * drop down items as necessary.
 * @param {*} props 
 * @returns The item represented by each box in the dropdown menu
 */
export default function DropdownItem(props) {

  // utiliized in the getPeople function to get the appropriate data
  const msal = useMsal();

  // axios get request to retrieve the appropriate data from the Person table
  // then calls updateOldRecord() to update the previous most recent history 
  async function getPeople(item, name) {
    await Axios.get(`${process.env.REACT_APP_API_SERVICE_PERSON}`).then(
      (response) => {
        var email = msal.accounts[0].username
        for (var key in response.data) {
          if (email === response.data[key].email) {
            updateOldRecord(item, name, response.data[key].id)
          }
        }
      }
    )
  }

  // axios put request to update the appropriate VIN number with the correct days in offline
  // TODO: implement logic that updates the days in offline in real time
  async function updateTruck(truckVIN, days) {
    if (days === 0) {
      await Axios.put(`${process.env.REACT_APP_API_SERVICE_TRUCK}/${truckVIN}`, {
        VIN: truckVIN,
        days_in_Offline: days + 1
      })
      .then(response => console.log(response))
      .catch(error => console.log(error))
    } else {
      // do nothing?
    }
  }

  // axios get request to grab the appropriate VIN number from the database
  async function getTruck(VIN) {
    await Axios.get(`${process.env.REACT_APP_API_SERVICE_TRUCK}/${VIN}`).then(
      (response) => {
        updateTruck(response.data.vin, response.data.days_in_Offline)
        window.location.reload(false)
      }
    )
  }
  
  // start of the update process. need to get the history of a truck first before 
  // updating the history to make sure that the history exists.
  async function updateOldRecord(item, name, id) { 
    await Axios.get(`${process.env.REACT_APP_API_SERVICE_HISTORY}/${item.VIN}`).then(
      (response) => {
        if (response.data.length === 0) {
          postRequestNewRecord(item, name, id)
        } else {
          for (var key in response.data) {
            if (response.data[key].move_Out === null) {
              putRequest(response.data[key], item, name, id)
            }
          }
        }
      }
    ).catch(error => console.log(error))
  }

  // axios put request updating the move_Out column with the appropriate datetime value.
  async function putRequest(data, item, name, id) {
    var tzoffset = (new Date()).getTimezoneOffset() * 60000; //offset in milliseconds
    var localISOTime = (new Date(Date.now() - tzoffset)).toISOString().slice(0, -1);
    await Axios.put(`${process.env.REACT_APP_API_SERVICE_HISTORY}/${data.historyId}`, {
      "historyId": data.historyId,
      "loc": data.loc,
      "comments": data.comments,
      "move_In": data.move_In,
      "move_Out": localISOTime, // parameter we are updating
      "total_Time": data.total_Time,
      "truckVIN": data.truckVIN,
      "stationId": data.stationId,
      "personId": data.personId
    })
    .then((response) => {
      postRequestNewRecord(item, name, id)
    })
    .catch(error => console.log(error))
  }

  async function postRequestNewRecord(item, name, id) {
    // Axios post to insert new record with move in time
    var tzoffset = (new Date()).getTimezoneOffset() * 60000; //offset in milliseconds
    var localISOTime = (new Date(Date.now() - tzoffset)).toISOString().slice(0, -1);
    name = name.replaceAll(" ", "_")
    await Axios.post(`${process.env.REACT_APP_API_SERVICE_HISTORY}`, {
        "loc": name,
        "comments": "test", // needs to be updated with user comments
        "move_In": localISOTime,
        "move_Out": null,
        "total_Time": null,
        "truckVIN": item.VIN,
        "stationId": name,
        "personId": id
    })
    .then(response => getTruck(item.VIN))
    .catch(error => console.log(error))
  }
  
  // Axios get request to retrieve the entire history of a specific VIN number
  async function getRequest(item, name) {
    await Axios.get(`${process.env.REACT_APP_API_SERVICE_HISTORY}/${item}`).then(
      (response) => {
        if (response.data.length === 0) {
          getPeople(item, name)  
        } else {
          //do nothing?
        }
      }
    ) 
  }

  // const for utilizing the dragging portion of the DnDProvider.
  const [{isDragging}, drag] = useDrag(() => ({
    type: 'div',
    item: {VIN: props.vin},
    collect: (monitor) => ({
      isDragging: monitor.isDragging(),
    }),
    end: (item, monitor) => {
      const dropResult = monitor.getDropResult();
      if (item && dropResult) {
        var cleanName = dropResult.name.replace(" ", "_")
        cleanName = cleanName.toLowerCase();
        let text = "Are you sure you want to drop VIN number " + item.VIN.substring(14) + " in " + dropResult.name + "?"
        if (window.confirm(text) === true) {
          //input more logic here
          console.log("Confirmed")
          // Axios put to update move out time
          getRequest(item, cleanName)
          // Move out time of old record and move in time of new record should be the same
        } else {
          console.log("Denied")
          //Do nothing!
        }
      }
    }
  }))
  var opacity = isDragging ? 0 : 1

  return (
    <h4 ref={drag} style={{opacity:opacity}} className={DropdownStyle.dropdowncontent}>{props.vin}</h4>
  )
}
