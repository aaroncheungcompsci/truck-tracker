import React from 'react'
import TableStyle from '../css/table.module.css';

/**
 * Table that holds all the trucks currently in the assembly line. 
 * Not implemented yet, but should have live updates according to
 * the appropriate database that this function will pull from
 * once access is obtained.
 * @returns the table that shows all the trucks that are currently
 * in the assembly line.
 */
export default function TruckTable() {
  return (
    <>
    <div>
        <table className={TableStyle.table}>
          <tbody>
            <tr> 
                <th className={TableStyle.headers}>Station</th>
                <th className={TableStyle.headers}>Trucks in Line</th>
            </tr>
            <tr className={TableStyle.data}>
                <td>15</td>
                <td>Test</td>
            </tr>
            <tr className={TableStyle.data}>
                <td>14</td>
                <td>Test</td>
            </tr>
            <tr className={TableStyle.data}>
                <td>13</td>
                <td>Test</td>
            </tr>
            <tr className={TableStyle.data}>
                <td>12</td>
                <td>Test</td>
            </tr>
            <tr className={TableStyle.data}>
                <td>11</td>
                <td>Test</td>
            </tr>
            <tr className={TableStyle.data}>
                <td>10</td>
                <td>Test</td>
            </tr>
            <tr className={TableStyle.data}>
                <td>9</td>
                <td>Test</td>
            </tr>
            <tr className={TableStyle.data}>
                <td>8</td>
                <td>Test</td>
            </tr>
            <tr className={TableStyle.data}>
                <td>7</td>
                <td>Test</td>
            </tr>
            <tr className={TableStyle.data}>
                <td>6</td>
                <td>Test</td>
            </tr>
            <tr className={TableStyle.data}>
                <td>5</td>
                <td>Test</td>
            </tr>
            <tr className={TableStyle.data}>
                <td>4</td>
                <td>Test</td>
            </tr>
            <tr className={TableStyle.data}>
                <td>3</td>
                <td>Test</td>
            </tr>
            <tr className={TableStyle.data}>
                <td>2</td>
                <td>Test</td>
            </tr>
            <tr className={TableStyle.data}>
                <td>1</td>
                <td>Test</td>
            </tr>
            <tr>

            </tr>
          </tbody>
        </table>
    </div>
    
    </>
  )
}
