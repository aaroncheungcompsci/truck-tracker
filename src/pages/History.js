import React, {useState} from 'react'
import HistoryStyle from '../css/history.module.css'
import SearchTruck from '../components/SearchTruck'
import Axios from 'axios'

/**
 * Responsible for displaying the requested data in a table.
 * @returns a page with a table containing the requested VIN number's history
 */
export default function History() {
  const [VIN, setVIN] = useState("");
  let history = []

  const handleChange = e => {
    setVIN(e.target.value)
    getRidofText()
  }

  const SendRequest = () => {
    getRidofText()
    removeHistory()
    Axios.get(`${process.env.REACT_APP_API_SERVICE_HISTORY}/${VIN}`).then(
        (response) => {
          if (response.data.length === 0) {
            errorText()
            return
          }
          //sort data here before displaying on table
          history = Object.values(response.data)
          history.sort((a, b) => new Date(b.move_In) - new Date(a.move_In))
          for (var key in history) {
            addRowtoTable(Object.values(history[key]))
          }
        }
      )
      .catch(error => 
        errorText(error)
      )
  }

  function errorText(error) {
    document.getElementById("error").innerHTML = "Invalid Truck VIN!"
  }

  function getRidofText() {
    document.getElementById("error").innerHTML = ""
  }

  function removeHistory() {
    let tableRef = document.getElementById('trucktable')
    if (tableRef.rows.length === 1) {
      return
    } else {
      for (var i = tableRef.rows.length - 1; i > 1; i--) {
        tableRef.deleteRow(i)
      }
    }
  }

  function cleanString(time) {
    if (time === null) {
      return "None"
    }
    var cleanTime = time.substring(0, time.indexOf('.'))
    cleanTime = cleanTime.replaceAll("T", " ")
    return cleanTime
  }

  async function addRowtoTable(data) {
    let tableRef = document.getElementById('trucktable')
    let newRow = tableRef.insertRow(-1)
    let formattedTime

    let stationCell = newRow.insertCell(0)
    let locationCell = newRow.insertCell(1)
    let moveInCell = newRow.insertCell(2)
    let moveOutCell = newRow.insertCell(3)
    let totalTimeCell = newRow.insertCell(4)
    let commentsCell = newRow.insertCell(5)
    let personCell = newRow.insertCell(6)

    let stationText = data[7].charAt(0).toUpperCase() + data[7].slice(1);
    stationText = stationText.replaceAll("_", " ");
    stationText = document.createTextNode(stationText);
    stationCell.appendChild(stationText);

    let locationText = data[1].charAt(0).toUpperCase() + data[1].slice(1);
    locationText = locationText.replaceAll("_", " ");
    locationText = document.createTextNode(locationText);
    locationCell.appendChild(locationText);

    let commentsText = document.createTextNode(data[2]);
    commentsCell.appendChild(commentsText);

    var moveInDate = new Date(data[3]).toLocaleString('en-US', {timeZone: "US/Arizona"})
    var cleanTime = cleanString(data[3])
    let moveInText = document.createTextNode(moveInDate);
    moveInCell.appendChild(moveInText);

    let moveOutDate
    if (data[4] === null) {
      moveOutDate = "None"
    } else {
      moveOutDate = new Date(data[4]).toLocaleString('en-US', {timeZone: "US/Arizona"})
    }
    cleanTime = cleanString(data[4])
    let moveOutText = document.createTextNode(moveOutDate);
    moveOutCell.appendChild(moveOutText);

    if (data[5] >= 86400) {
      var seconds = data[5]
      var hours = Math.floor(seconds / 3600)
      var minutes = Math.floor((data[5]/60) - (hours * 60))
      seconds = data[5] % 60

      formattedTime = hours.toString().padStart(2, '0') +
      ':' + minutes.toString().padStart(2, '0') +
        ':' + seconds.toString().padStart(2, '0');
    } else {
      formattedTime = new Date(data[5] * 1000).toISOString().substring(11, 19);
    }
    
  
    let totalTimeText = document.createTextNode(formattedTime); //convert to days hours mins
    totalTimeCell.appendChild(totalTimeText);
    
    Axios.get(`${process.env.REACT_APP_API_SERVICE_PERSON}/${data[8]}`).then(
      (response) => {
        var email = response.data.email
        let personText = document.createTextNode(email);
        personCell.appendChild(personText)    
      }
    )
  }

  return (
    <>
    <div>
        <h1 className={HistoryStyle.title}>Truck History</h1>

        <SearchTruck
          handleClick={SendRequest.bind(this)}
          handleChange={handleChange.bind(this)}
          VIN={VIN}
        />

        <table className={HistoryStyle.trucktable} id='trucktable'>
          <tbody>
            <tr className={HistoryStyle.columnheaders}> 
                <th>Station</th>
                <th>Location</th>
                <th>Move in Date</th>
                <th>Move out Date</th>
                <th>Total Time (HH:MM:SS)</th>
                <th>Comments</th>
                <th>Responsible Person</th>
            </tr>
            <tr bgcolor="lightgrey" id='truckdata'>
            </tr>
          </tbody>
        </table>
    </div>
    
    </>
    

  )
}
