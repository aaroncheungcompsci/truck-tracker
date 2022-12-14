import React from 'react'
import OfflineTrucks from '../components/OfflineTrucks'
import OnlineTrucks from '../components/OnlineTrucks'

/**
 * Responsible for displaying the homepage of the web application
 * @param {*} props 
 * @returns the homepage component
 */
export default function Home(props) {
  return (
    <>
        <OfflineTrucks data={props.data}/>
        <OnlineTrucks />
    </>
  )
}