import './Airport.css'
import React,{useEffect, useState,useContext} from 'react'
import { StationsContext } from "../Context/StationsContext";
import Station from './Station'
import Grid from '@mui/material/Grid';
import AirplaneSvg from '../Assets/Plane.svg'
import Terminal from '../Assets/Terminal.jpg'
import Airplane from './Airplane'

const Airport = () => {


    const {stations,SetStations} = useContext(StationsContext);

    const renderTopStations= (array) => {
        return array.map((s,idx)=> {
            let id;
            id = s.id;
            if(id <= 4){
            return (
                <Grid item xs={3}>
                    <Station stationId={id}>
                        {s.airplain.id > 0 ? 
                        <Airplane transform={270} airplaneId={s.airplain.airplainNumber}/> :
                        null}
                    </Station>
                </Grid>
                )}
        });
    }

    const renderMidStations= (array) => {
        return array.map((s,idx)=> {
            let id, trans;
            id = s.id;
            trans = (s.airplain.airplaneState === 0 && id===8) ? 300: 160
            if(id === 5 || id === 8){
                return (
            <Grid item xs={6}>
            <Station stationId={id}>
                <div>
                    {s.airplain.id > 0 ?
                    <Airplane transform={trans} airplaneId={s.airplain.airplainNumber}/> :
                    null}
                </div>
                </Station>
            </Grid>
                )}
        });
    }

    const renderBotStations= (array) => {
        return array.map((s,idx)=> {
            let id, trans, state;
            id = s.id;
            state = s.airplain.airplaneState;
            trans = (id===7) ? 
                            state === 0 ? 40 : 160: state===0 ? 320 : 140
            if(id === 6 || id === 7){
                return (
            <Grid container xs={6} 
            justifyContent="center"
            alignItems="center">
            <Station stationId={id}>
                {s.airplain.id > 0 ?
                            <Airplane transform={trans} airplaneId={s.airplain.airplainNumber}/> :
                            null}
                </Station>
            </Grid>
                )}
        });
    }

    return (
        <div className="Airport">
            <Grid container padding={2} justifyContent="center">
                <Grid container spacing={1} padding={2} direction="row-reverse">
                        {renderTopStations(stations)}
                </Grid>

                <Grid container spacing={1} padding={2}  justifyContent="space-evenly" >
                        {renderMidStations(stations)}
                </Grid>

                <Grid container spacing={1} padding={2} justifyContent="space-evenly">
                        {renderBotStations(stations)}
                </Grid>

                    <Grid item xs={6} justifyContent="center">
                        <Station stationId={'Terminal'}><img style={{width:"50vw"}} src={Terminal}/></Station>
                    </Grid>
            </Grid>
        </div>
    )
}

export default Airport;