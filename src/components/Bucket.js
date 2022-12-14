import React from 'react'
import Truck from './Truck'
import BucketStyle from '../css/bucket.module.css'
import NumberOfTrucks from './NumberOfTrucks';
import { useState, useEffect } from 'react';
import { useDrop } from 'react-dnd';
import Axios from 'axios';


/**
 * This component is responsible for displaying the buckets properly
 * in the homepage.
 * @param {*} props 
 * @returns 1 of 3 things. If the bucket is the placeholder bucket,
 * it returns the appropriate styling, name, and component. The same is true for the
 * other two return statements.
 */
export default function Bucket(props) {
  let redColor
  /**
   * const for utilizing the DnDProvider to allow
   * the drag and drop to work.
   */
  const [{canDrop, isOver}, drop] = useDrop(() => ({
    accept: "div",
    drop: (div) => ({name: props.name}),
    collect: monitor => ({
      isOver: monitor.isOver(),
      canDrop: monitor.canDrop(),
    })
  }))

  /**
   * const for loading the data on component render
   */
  const [data, setData] = useState([]);

  /**
   * sets the appropriate state for the data
   */
  useEffect(() => {
    getData();
  }, []);

  /**
   * function that utilizes Axios for getting all the stations
   * from the backend.
   */
  const getData = () => {
    Axios.get(`${process.env.REACT_APP_API_SERVICE_STATION}`).then(
        (response) => {
          setData(response.data)
        }
      )
      .catch(error => console.log(error))
  }

  /**
   * comparing string names to get appropriate data
   */
  var currentNumber = 0;
  for (var key in data) {
    var data1 = data[key].stationId;
    data1 = data1.replaceAll("_", "");
    data1 = data1.toLowerCase();

    var data2 = props.name;
    data2 = data2.replaceAll(" ", "");
    data2 = data2.toLowerCase();

    if (data1 === data2) {
      currentNumber = data[key].num_of_Current_Trucks
      if (currentNumber > data[key].num_of_Allowed_Trucks || props.name === "Holds") {
        redColor = true
      }
    }
  }

  if (props.name === "TO BE PLACED") {
    return (
      <div className={BucketStyle.assembly_bucket}>
        <div className={BucketStyle.title}>
          {props.name}
        </div>
        <Truck/>
      </div>
    )
  } else if (props.name !== "Ok to Ship" && props.name !== "Shipped") {
    return (
      <div className={BucketStyle.bucket} ref={drop}>
        <div className={redColor ? BucketStyle.redtitle : BucketStyle.title}>
          {props.name} {props.number}
        </div>
        <NumberOfTrucks number={currentNumber} name={props.name}/>
      </div>
    )
  } else {
    return (
      <div className={BucketStyle.bucket} ref={drop}>
        <div className={BucketStyle.greentitle}>
          {props.name} {props.number}
        </div>
        <NumberOfTrucks number={currentNumber} name={props.name}/>
      </div>
    )
  }
}