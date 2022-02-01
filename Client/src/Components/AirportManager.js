import {  HubConnectionBuilder, JsonHubProtocol, LogLevel } from "@microsoft/signalr";
import{ useEffect , useContext, useState, useMemo } from "react";
import {StationsContext} from '../Context/StationsContext';
import {WaitingAirplanesContext} from '../Context/WaitingAirplanesContext';
import Draw from'./FlightsDrawer';
import Airport from'./Airport';

const AirportManager = (props) => {

    const [connection, setConnection] = useState(); 
    const [stations, setStations] = useState([]); 
    const [airplanes, setAirplanes] = useState([]); 


    const Stations = useMemo(() => ({stations, setStations}), [stations, setStations])
    const Airplanes = useMemo(() => ({airplanes, setAirplanes}), [airplanes, setAirplanes])

    useEffect(()=>{
        connectToServer();
    },[])

    const connectToServer = async () => {
        try{
          const connection = new HubConnectionBuilder()
          .withUrl(`http://localhost:5500/airport`)
          .configureLogging(LogLevel.Information)
          .build();

          connection.on("AirplanesOnTrack", (res) => {
            // console.log(res)
            setStations(res)
          });

          connection.on("WaitingAirplanes", (res) => {
            // console.log(res)
            setAirplanes(res)
          });

          connection.onclose(e => {
            setConnection();
          })
          
          await connection.start();
          await connection.invoke("ConnectAsync")

          setConnection(connection);
          
        } catch(e){
          console.log(e);
        }
      }

    return (
        <WaitingAirplanesContext.Provider value={Airplanes}>
            <Draw>
                <StationsContext.Provider value={Stations}>
                    <Airport/>
                </StationsContext.Provider> 
            </Draw>
        </WaitingAirplanesContext.Provider> 
    )
}

export default AirportManager;


