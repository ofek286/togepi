import React, { Component } from 'react';

import { Map, GoogleApiWrapper } from 'google-maps-react';

const AnyReactComponent = ({ text }) => <div>{text}</div>;

const mapStyles = {
  width: '40%',
  height: '40%',
  maxWidth: '600px',
  maxHeight: '600px'

};


class CustomGoogleMap extends Component {


render() {
  return (
        <Map
          google={this.props.google}
          zoom={5}
          style={mapStyles}
          initialCenter={{ lat: 40.444, lng: -74.176}}
        />
    );
}
}

export default GoogleApiWrapper({
  apiKey: 'AIzaSyDAUvmu8f4Vn4q3hiPbWzT_u84fOYCgQ6g'
})(CustomGoogleMap);
