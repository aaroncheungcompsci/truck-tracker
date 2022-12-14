import React, { useEffect, useState } from 'react'
import TruckStyle from '../css/trucks.module.css'
import DropdownItem from './DropdownItem';
import DropdownStyle from '../css/dropdown.module.css'
import Axios from 'axios';

// THIS IS WHERE YOU GET THE DATA OF TRUCKS INLINE FROM DATABASE
/**
 * Responsible for showing the list of trucks to be dragged/dropped
 * in the placeholder bucket.
 * @returns the appropriate dropdown items
 */
export default function Truck() {

  const [display, setDisplay] = useState("none")
  const [trucks, setTrucks] = useState([])

  // gets the data from the truck database on initial render
  useEffect(() => {
    getTrucks()
  }, [])

  const getTrucks = () => {
    let truckList = []
    Axios.get(`${process.env.REACT_APP_API_SERVICE_TRUCK}`).then(
      (response) => {
        for (var key in response.data) {
          if (response.data[key].days_in_Offline === 0) {
            truckList.push(response.data[key])
          } // else do nothing
        }
        truckList.sort((a, b) => a.vin.substring(14) - b.vin.substring(14) )
        setTrucks(truckList)
      }
    )
  }

  // handles the display of the drop down menu items when user clicks button
  function handleClick() {
    if (display === "none") {
        setDisplay("block")
    } else {
        setDisplay("none")
    }
  }

  let dropDownComponent 
  // displays items if there are items, otherwise displays "No trucks left" instead
  if (trucks.length !== 0) {
    dropDownComponent = trucks.map((truck, key)  => <DropdownItem key={key} vin={truck.vin}/>)
  } else {
    dropDownComponent = <h3>No trucks left!</h3>
  }
  return (
    <div className={TruckStyle.onlinecontent} id="Dropdown">
      <button onClick={handleClick}>
          Click here to see list of trucks
      </button>
      <div style={{display:display}} className={DropdownStyle.dropdown}>
          {dropDownComponent}
      </div>
    </div>
  )
}
