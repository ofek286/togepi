import React from 'react';
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


import { GoogleMap, LoadScript } from '@react-google-maps/api'

import Container from '@material-ui/core/Container';

import Box from '@material-ui/core/Box';

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

function Content(props) {
  const { classes } = props;

  return (

    <Paper className={classes.paper}>

      <AppBar className={classes.searchBar} position="static" color="default" elevation={0}>
        <Toolbar>
          <Grid container spacing={2} alignItems="center">

            <Grid item>

              <Tooltip title="Reload">
                <IconButton>
                  <RefreshIcon className={classes.block} color="inherit" />
                </IconButton>
              </Tooltip>
            </Grid>
          </Grid>
        </Toolbar>
      </AppBar>

      <div className={classes.contentWrapper}>
      <Grid container spacing={0} alignItems="center">

      <Grid item xs={4} style={{height:500 +'px'}}>
        <Container maxWidth="sm">
              <CustomeDataTable/>
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

Content.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(Content);
