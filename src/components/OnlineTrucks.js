import React from 'react'
import OnlineGrid from './OnlineGrid'

import AssemblyBoxStyle from '../css/assemblybox.module.css'

/**
 * Responsible for providing the template for the online grid component
 * @returns a "box" that holds the online grid
 */

export default function OnlineTrucks() {
  return (
    <>
      <h2 className={AssemblyBoxStyle.top}>TRUCKS INLINE</h2>
      <div className={AssemblyBoxStyle.box}>
        <OnlineGrid />
      </div>
    </>
  )
}