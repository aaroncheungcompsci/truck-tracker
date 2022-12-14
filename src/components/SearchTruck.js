import React from 'react'
import TruckStyle from '../css/trucks.module.css'

export default function SearchTruck(props) {

  /**
   * Most of the props that is passed into this React component is from
   * History.js located in the "pages" directory. There is another reference
   * to this component in OfflineTrucks.js, but it currently does not do anything.
   */
  return (
    <>
        <form className={TruckStyle.search}>
            <label htmlFor="VIN">VIN Number: </label>
            <input type="text" id="VIN" name="VIN" value={props.VIN} onChange={props.handleChange}/>
            <input type="button" value="Submit" onClick={props.handleClick}/>
        </form>
        <p id='error' className={TruckStyle.error}></p>
    </>
  )
}
