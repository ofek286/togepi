import React from 'react';
import ReactDOM from 'react-dom';

import DataTable, { memoize }from 'react-data-table-component';



const CustomEvent = ({ row }) => (
  <div>
    {}
    <div>{row}</div>
    </div>
);

const CustomButton = ({ row }) => (
  <div>
    {}
    <div><button>{row}</button></div>
    </div>
);

const CustomLocation = ({ row }) => (
  <div>
    <div>
      <div style={{ color: 'grey', overflow: 'hidden', whiteSpace: 'wrap', textOverflow: 'ellipses' }}>
        {}
        {row}
      </div>
      </div>
  </div>

);


class CustomeDataTable extends React.Component {
  handleInput = (e) => {
     console.log(e.target.id);
     this.state.getPressedEventIdFunction(e.target.id);
 }

  constructor(props) {
    super(props);
    this.state = {
      data: this.props.data,
      getPressedEventIdFunction: this.props.getEventId,
      columns :[
        {
          name: 'Event Type',
          selector: 'eventType',

          maxWidth: '100px', // when using custom you should use width or maxWidth, otherwise, the table will default to flex grow behavior
          cell: row => <CustomEvent row={row.eventType}/>,
        },
        {
          name: 'Location',
          selector: 'eventLocation',
          maxWidth: '900px', // when using custom you should use width or maxWidth, otherwise, the table will default to flex grow behavior
          cell: row => <CustomEvent row={row.eventLocation} />,

        },
        {
          name: 'View Event',
          selector: 'eventId',
          maxWidth: '150px', // when using custom you should use width or maxWidth, otherwise, the table will default to flex grow behavior
          cell: row => <button style={{width:100+"px", height:35+"px"}} id={row.eventId} onClick={this.handleInput}>View</button>,
        },
      ]

    };

  }

  render() {
    return (
      <div>

      <DataTable
        title='Current Active Events'
        columns={this.state.columns}
        data={this.state.data}/>
      </div>
    );
  }
}

export default CustomeDataTable;
