import React from 'react'
import Bucket from './Bucket'
import GridStyle from '../css/grid.module.css'
import { ArcherContainer, ArcherElement } from 'react-archer'
import TruckTable from './TruckTable'

/**
 * Responsible for displaying the trucks currently in the assembly line as well
 * as holding trucks that move off of station 15 in a placeholder bucket named
 * "to be placed"
 * @param {*} props 
 * @returns the "grid" for the online trucks
 */

export default function OnlineGrid(props) {
    return (
        <ArcherContainer strokeColor="red">
            <div className={GridStyle.grid}>
                <ArcherElement
                    id="placeholder"
                >
                    <div className={GridStyle.end}>
                        <Bucket name="TO BE PLACED"/>
                    </div>
                </ArcherElement>

                <ArcherElement
                    id="listoftrucks"
                    relations={[{
                        targetId: 'placeholder',
                        targetAnchor: 'bottom',
                        sourceAnchor: 'top',
                        style:{strokeWidth: 1}
                    }]} 
                >
                    <div className={GridStyle.start}>
                        <TruckTable/>
                    </div>
                </ArcherElement>
                
            </div>
        </ArcherContainer>
    )
}
