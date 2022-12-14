import React from 'react'
import InfoBox from './InfoBox'
import Bucket from './Bucket'
import GridStyle from '../css/grid.module.css'
import { ArcherContainer, ArcherElement } from 'react-archer'

/**
 * Responsible for displaying the appropriate amount of buckets as boxes
 * and arrows in between each bucket to indicate the process flow
 * @param {*} props 
 * @returns the entire grid of all the offline trucks 
 */

export default function OfflineGrid(props) {
    var arrowStyle = {
        strokeWidth: 1
    }

    return (
        <ArcherContainer>
            <div className={GridStyle.grid}>
                <ArcherElement
                    id='deadtruck'
                    relations={[
                        {
                            targetId: 'align',
                            targetAnchor: 'top',
                            sourceAnchor: 'bottom',
                            style: arrowStyle
                        }
                    ]}
                >   
                    <div>
                    <Bucket name="Dead Truck" number="0" />
                    </div>
                </ArcherElement>

                <ArcherElement
                    id='beforerepair'
                    relations={[
                        {
                            targetId: 'dyno',
                            targetAnchor: 'top',
                            sourceAnchor: 'bottom',
                            style: arrowStyle
                        }
                    ]}
                >
                    <div>
                        <Bucket name="Before Repair" number="0"/>
                    </div>
                </ArcherElement>

                <div className={GridStyle.end}>
                    <InfoBox/>
                </div>

                <ArcherElement
                    id='align'
                    relations={[       
                        {
                            targetId: 'dyno',
                            targetAnchor: 'left',
                            sourceAnchor: 'right',
                            style: arrowStyle
                        }
                    ]}
                >
                    <div className={GridStyle.start}>
                        <Bucket name="Align" number="1"/>
                    </div>
                </ArcherElement>

                <ArcherElement
                    id='dyno'
                    relations={[
                        {
                            targetId: "provision",
                            targetAnchor: 'left',
                            sourceAnchor: 'right',
                            style: arrowStyle
                        }
                    ]}
                >
                    <div>
                        <Bucket name="Dyno" number="1"/>
                    </div>
                </ArcherElement>

                <ArcherElement
                    id='provision'
                    relations={[
                        {
                            targetId: 'endofline',
                            targetAnchor: 'left',
                            sourceAnchor: 'right',
                            style: arrowStyle
                        }
                    ]}
                >
                    <div>
                        <Bucket name="Provision" number="1"/>
                    </div>
                </ArcherElement>

                <ArcherElement
                    id='endofline'
                    relations={[
                        {
                            targetId: 'charging',
                            targetAnchor: 'left',
                            sourceAnchor: 'right',
                            style: arrowStyle
                        }
                    ]}
                >
                    <div>
                        <Bucket name="End of Line" number="2"/>
                    </div>
                </ArcherElement>

                <ArcherElement
                    id='charging'
                    relations={[
                        {
                            targetId: 'driveaudit',
                            targetAnchor: 'left',
                            sourceAnchor: 'right',
                            style: arrowStyle
                        }
                    ]}
                >
                    <div>
                        <Bucket name="Charging" number="1"/>
                    </div>
                </ArcherElement>   

                <ArcherElement
                    id="driveaudit"
                    relations={[
                        {
                            targetId: 'pdi',
                            targetAnchor: 'top',
                            sourceAnchor: 'bottom',
                            style: arrowStyle
                        }
                    ]}
                >
                    <div className={GridStyle.end}>
                        <Bucket name="Drive Audit" number="1"/>
                    </div>                
                </ArcherElement>

                <ArcherElement
                    id='oktoship'
                    relations={[
                        {
                            targetId: 'shipped',
                            targetAnchor: 'top',
                            sourceAnchor: 'bottom',
                            style: arrowStyle
                        }
                    ]}
                >
                    <div className={GridStyle.start}>
                        <Bucket name="Ok to Ship"/>
                    </div>
                </ArcherElement>

                <ArcherElement
                    id='cparepair'
                    relations={[
                        {
                            targetId: 'oktoship',
                            targetAnchor: 'right',
                            sourceAnchor: 'left',
                            style: arrowStyle
                        }
                    ]}
                >
                    <div>
                        <Bucket name="CPA Repair" number="1"/>
                    </div>
                </ArcherElement>

                <ArcherElement
                    id='cpa'
                    relations={[
                        {
                            targetId: 'cparepair',
                            targetAnchor: 'right',
                            sourceAnchor: 'left',
                            style: arrowStyle
                        }
                    ]}

                >
                    <div>
                        <Bucket name="CPA" number="1"/>
                    </div>
                </ArcherElement>

                <ArcherElement
                    id='repval'
                    relations={[
                        {
                            targetId: 'cpa',
                            targetAnchor: 'right',
                            sourceAnchor: 'left',
                            style: arrowStyle
                        }
                    ]}
                >
                    <div>
                        <Bucket name="Repair Validation" number="1"/>
                    </div>   
                </ArcherElement>

                <ArcherElement
                    id='afterrepair'
                    relations={[
                        {
                            targetId: 'repval',
                            targetAnchor: 'right',
                            sourceAnchor:'left',
                            style: arrowStyle                        
                        }
                    ]}
                >
                    <div>
                        <Bucket name="After Repair" number="4"/>
                    </div>
                </ArcherElement>
                
                <ArcherElement
                    id='pdi'
                    relations={[
                        {
                            targetId: 'afterrepair',
                            targetAnchor: 'right',
                            sourceAnchor:'left',
                            style: arrowStyle
                        }
                    ]}
                >
                    <div className={GridStyle.end}>
                        <Bucket name="PDI" number="1"/>
                    </div>
                </ArcherElement>
                
                <ArcherElement
                    id='shipped'>
                    <div>
                        <Bucket name="Shipped"/>
                    </div>
                </ArcherElement>
                
                <div className={GridStyle.end}>
                    <Bucket name="Holds"/>
                </div>
            </div>
        </ArcherContainer>
    )
}
