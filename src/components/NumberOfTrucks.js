import React, {useState, useEffect} from 'react'
import TruckStyle from '../css/trucks.module.css'
import DropdownStyle from '../css/dropdown.module.css'

import DropdownItem from './DropdownItem'
import Axios from 'axios'

/**
 * Responsible for displaying the appropriate number as well as the appropriate
 * number of items when a dropdown list appears.
 * @param {*} props 
 * @returns the appropriate amount of dropdown items dependent on the number shown on the bucket
 */
export default function NumberOfTrucks(props) { // props is number passed in from database

  const [display, setDisplay] = useState("none")
  const [truckHistory, setTruckHistory] = useState([])
  const [loading, setLoading] = useState(true)
  let trucks = []

  // handles the button click for when the user clicks on the button on a bucket
  function handleClick() {
    if (display === "none") {
        setDisplay("block")
    } else {
        setDisplay("none")
    }
  }

  // fetches data only on initial render
  useEffect(() => {
    getTrucks()
  }, [])

  // get data const
  const getTrucks = () => {
    let truckList = []
    Axios.get(`${process.env.REACT_APP_API_SERVICE_TRUCK}`).then(
      (response) => {
        for (var key in response.data) {
          if (response.data[key].days_in_Offline !== 0) {
            truckList.push(response.data[key])
          } else {
            setLoading(false)
          }
        }
        for (key in truckList) {
          getHistoryOfTrucks(truckList[key].vin)
        }
      }
    )
  }

  // Axios get request to retrieve the entire history of a VIN number, and extracts
  // the stationId and move_Out properties from the object
  async function getHistoryOfTrucks(VIN) {
    let truckHistory = []
      await Axios.get(`${process.env.REACT_APP_API_SERVICE_HISTORY}/${VIN}`).then(
        (response) => {
          for (var key in response.data) {
            if (response.data[key].move_Out === null) {
              truckHistory.push({
                stationId: response.data[key].stationId,
                truckVIN: VIN,
                move_Out: response.data[key].move_Out
              })
            }
            setTruckHistory((prevState) => ( 
              [...prevState, truckHistory]
            ))
          }
          setLoading(false)
        }
      ).catch(error => console.log(error))
  }

  // if data is not retrieved yet, component will return a loading indicator
  if (loading) {
    return <div>Loading...</div>
  } else {
    //only allows one instance of VIN number in offline trucks.
    for (var key in truckHistory) {
      for (var prop in truckHistory[key]) {
        if (truckHistory[key][prop].stationId === props.name.toLowerCase().replaceAll(" ", "_") &&
              truckHistory[key][prop].move_Out === null &&
                !trucks.includes(truckHistory[key][prop].truckVIN)) { 
                  trucks.push(truckHistory[key][prop].truckVIN)
        } else {
          continue
        }
      }
    }
    const dropDownComponent = trucks.map((vin, key)  => <DropdownItem key={key} vin={vin}/>)
    
    return (
      <div className={TruckStyle.offlinecontent}>
          <button className={TruckStyle.offlinebutton} onClick={handleClick}>{props.number}</button>
          <div style={{display:display}} className={DropdownStyle.dropdown} id='truckdropdown'>
            {dropDownComponent}
          </div>
      </div>
    )
  }
}
