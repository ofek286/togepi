import React from 'react';
import ReactDOM from 'react-dom';

import DataTable, { memoize }from 'react-data-table-component';



const CustomEvent = ({ row }) => (
  <div>
    {}
    <div>{row}</div>
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


class CustomeReportsTable extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      data: this.props.data,
      columns :[
        {
          name: 'Message',
          selector: 'content',
          wrap: true,
          format: row => <CustomEvent row={row.reportContent}/>,
        },
        {
          name: 'Time Received',
          selector: 'timeReceived',
          maxWidth: '400px', // when using custom you should use width or maxWidth, otherwise, the table will default to flex grow behavior
          cell: row => <CustomEvent row={row.reportTime} />,

        },
      ]

    };

    console.log('+++++++++++++= reports table data++++++++===');

    console.log(this.state.data);
    console.log('+++++++++++++= reports table data++++++++===');

  }

  render() {
    return (
      <div>

      <DataTable
        title='Reports About The Incident'
        columns={this.state.columns}
        data={this.state.data}/>
      </div>
    );
  }
}

export default CustomeReportsTable;
