import React, { Component } from 'react';
import PropTypes from 'prop-types';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Tooltip from '@material-ui/core/Tooltip';
import IconButton from '@material-ui/core/IconButton';
import { withStyles } from '@material-ui/core/styles';
import SearchIcon from '@material-ui/icons/Search';
import RefreshIcon from '@material-ui/icons/Refresh';

import CustomeDataTable from './CustomeDataTable.js';

import CustomGoogleMap from './CustomGoogleMap.js';

import loadinggif from '../resources/load.gif'

import { GoogleMap, LoadScript } from '@react-google-maps/api'

import Container from '@material-ui/core/Container';

import Box from '@material-ui/core/Box';

import axios from 'axios'


const styles = theme => ({
  paper: {
    maxWidth: 936,
    margin: 'auto',
    overflow: 'hidden',
  },
  searchBar: {
    borderBottom: '1px solid rgba(0, 0, 0, 0.12)',
  },
  searchInput: {
    fontSize: theme.typography.fontSize,
  },
  block: {
    display: 'block',
  },
  addUser: {
    marginRight: theme.spacing(1),
  },
  contentWrapper: {
    margin: '40px 16px',
  },
});

async function getEventsData (){
  console.log('Get Request Sent')
  await axios.get('https://togepi-backend.azurewebsites.net/api/events')
  .then(response => this.setState({username:response.data.events }))


}


class Content extends Component {
getEventsData2 = async () => {
    console.log('Get Request Sent')
    await axios.get('https://togepi-backend.azurewebsites.net/api/events')
    .then(response => this.setState({data:response.data.events }))

  }


  constructor(props) {
    super(props);
    this.state = {
      classes: this.props,
      isThereData: false,
    };
    if(this.state.isThereData == false){
    this.getEventsData2().then(response => {
      console.log(this.state.data)
  }).then(response => {this.setState({isThereData: true})})
  }
  }

render(){
  if (this.state.isThereData == false) {
    return(
      <div>
      <h1>Fetching Data From Server...</h1>
      <img src={loadinggif} alt="loading..." style={{width:500 +"px", height:400 + "px"}}/>

      </div>
    )
  }else{
    return (
    <Paper className={this.state.classes.paper}>

      <AppBar className={this.state.classes.searchBar} position="static" color="default" elevation={0}>
        <Toolbar>
          <Grid container spacing={2} alignItems="center">

            <Grid item>

              <Tooltip title="Reload">
                <IconButton>
                  <RefreshIcon className={this.state.classes.block} color="inherit" />
                </IconButton>
              </Tooltip>
            </Grid>
          </Grid>
        </Toolbar>
      </AppBar>

      <div className={this.state.classes.contentWrapper}>
      <Grid container spacing={0} alignItems="center">

      <Grid item xs={4} style={{height:500 +'px'}}>
        <Container maxWidth="sm">
              <CustomeDataTable  data={this.state.data}/>
        </Container>
      </Grid>

      <Grid item xs={8} style={{height: 450 + 'px', width: 350 + 'px'}}>
        <h1>Live Events Map</h1>
        <div >
        <CustomGoogleMap/>
        </div>
      </Grid>

      </Grid>
      </div>


    </Paper>
  );
}
}

}

Content.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(Content);
