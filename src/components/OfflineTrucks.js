import React from 'react'
import SearchTruck from './SearchTruck'
import OfflineGrid from './OfflineGrid'

import BucketBoxStyle from '../css/bucketbox.module.css'

/**
 * Responsible for providing the template for the offline grid component
 * @param {*} props 
 * @returns the box where the offline truck "grid" will be formatted
 */
  
export default function OfflineTrucks(props) {
  return (
    <>
      <div className={BucketBoxStyle.box}>
        <div className={BucketBoxStyle.top}>
          <h1>Trucks Offline</h1>
          <SearchTruck />
        </div>
        <OfflineGrid data={props.data}/>
      </div>
    </>
  )
}