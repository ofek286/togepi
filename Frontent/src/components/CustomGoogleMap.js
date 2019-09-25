import React, { Component } from 'react';

import { Map, GoogleApiWrapper,  Marker } from 'google-maps-react';

const AnyReactComponent = ({ text }) => <div>{text}</div>;

const mapStyles = {
  width: '40%',
  height: '40%',
  maxWidth: '800px',
  maxHeight: '800px'

};


class CustomGoogleMap extends Component {

  constructor(props) {
    super(props);
    this.state = {
      data: this.props.data
    };
    console.log(this.state.data);
    console.log("----------------------------")
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
  return (
        <Map
          google={this.props.google}
          zoom={10}
          style={mapStyles}
          initialCenter={{ lat: 40.744, lng: -73.916}}
        >
        {this.displayMarkers()}
        </Map>
    );
}
}

export default GoogleApiWrapper({
  apiKey: 'AIzaSyDAUvmu8f4Vn4q3hiPbWzT_u84fOYCgQ6g'
})(CustomGoogleMap);
