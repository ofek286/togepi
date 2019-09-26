import React, { Component } from 'react';

import { Map, GoogleApiWrapper,  Marker } from 'google-maps-react';

const AnyReactComponent = ({ text }) => <div>{text}</div>;

const mapStyles = {
  width: '40%',
  height: '40%',
  maxWidth: '800px',
  maxHeight: '800px'

};


class ReportEventMap extends Component {


  constructor(props) {
    super(props);
    this.state = {
      data: this.props.data,
      location:this.props.location,
      zoomSize:this.props.zoomSize,
      created:true
    };

    console.log("--------------location passed-------------")
    console.log(this.state.location);
    console.log("--------------zoomsize passed-------------")
    console.log(this.state.zoomSize);
  }

  displayMarkers = () => {
    console.log("creating markers")
    let divs = [];

    this.state.data.map(function(item, index) {
      console.log((item,index));
       divs.push(<Marker key={index} id={index} position={{
       lat: item.lat,
       lng: item.lng
     }}
     onClick={() => console.log("You clicked me!")}></Marker>)

   });
   return divs;

  }


render() {
  if(this.state.created == true){
    return(
      <Map
      google={this.props.google}
      zoom={this.state.zoomSize}
      style={mapStyles}
      initialCenter={{lat: this.state.location.lat, lng:this.state.location.lng}}
    >
    {this.displayMarkers()}
    </Map>
  );
  }
}
}

export default GoogleApiWrapper({
  apiKey: 'AIzaSyDAUvmu8f4Vn4q3hiPbWzT_u84fOYCgQ6g'
})(ReportEventMap);
