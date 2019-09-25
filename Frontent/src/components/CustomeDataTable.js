import React from 'react';
import ReactDOM from 'react-dom';

import DataTable from 'react-data-table-component';


const Button = () => (
  <button type="button">View</button>
);

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
const columns = [
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
    selector: 'id',
    maxWidth: '150px', // when using custom you should use width or maxWidth, otherwise, the table will default to flex grow behavior
    button: true,
    cell: () => <Button on>View</Button>,
  }


];

class CustomeDataTable extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      data: this.props.data,
      getPressedEventIdFunction: this.props.getEventId
    };

    this.state.getPressedEventIdFunction("afwafawwwadwafwafa");

  }

  render() {
    return (
      <div>

      <DataTable
        title='Current Active Events'
        columns={columns}
        data={this.state.data}
      onClick={this.state.getPressedEventIdFunction(11111111111)}/>
      </div>
    );
  }
}

export default CustomeDataTable;
