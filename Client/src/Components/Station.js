import "./Station.css";

const Station = (props) => {
    return (
        <div className="Station" style={{ backgroundRepeat: 'no-repeat',
                                          backgroundSize:'auto', 
                                          width:props.width,
                                          height:props.height }} > 
                                          <h3>
                                              Station-{props.stationId}
                                          </h3>
            {props.children}
        </div>
    )
}

export default Station;