import React from 'react'
import { useEffect, useState } from 'react'
import InfoBoxStyle from '../css/infobox.module.css'
import Axios from 'axios';

/**
 * Responsible for displaying the number of trucks in offline and 
 * the oldest truck in offline.
 * @returns the infobox showing how any trucks are in offline as well
 * as the oldest truck (second part has yet to be implemented)
 */

export default function InfoBox() {

  const [data, setData] = useState([]);

  // Retrieves station data on initial render
  useEffect(() => {
    getData();
  }, []);

  const getData = () => {
    Axios.get(`${process.env.REACT_APP_API_SERVICE_STATION}`).then(
        (response) => {
          setData(response.data)
        }
      )
      .catch(error => console.log(error))
  }

  // Displays the total trucks in offline. If any VIN number is in shipped,
  // the value is decremented to indicate only the trucks that are currently
  // in the offline buckets.
  var totalTrucks = 0;
  for (var key in data) {
    totalTrucks = totalTrucks + data[key].num_of_Current_Trucks;
    if (data[key].stationId === "shipped") {
      totalTrucks = totalTrucks - data[key].num_of_Current_Trucks;
    }
  }

  // Displays current total trucks in Offline.
  return (
    <div className={InfoBoxStyle.infobox}>
        <h3>Total Trucks in Offline: {totalTrucks}</h3>
        <h3>Oldest Truck: N/A</h3>
    </div>
  )
}
