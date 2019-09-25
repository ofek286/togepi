import React from 'react';
import ReactDOM from 'react-dom';

import DataTable from 'react-data-table-component';


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

const data = [{ id: 1, eventType: 'Conan the Barbarian', eventLocation: '1982' },{ id: 1, eventType: 'Conan the Barbarian', eventLocation: '1982' },
{ id: 1, eventType: 'Conan the Barbarian', eventLocation: '1982' },
{ id: 1, eventType: 'Conan the Barbarian', eventLocation: '1982' },
{ id: 1, eventType: 'Conan the Barbarian', eventLocation: '1982' },
{ id: 1, eventType: 'Conan the Barbarian', eventLocation: '1982' },
{ id: 1, eventType: 'Conan the Barbarian', eventLocation: '1982' },];
const columns = [
  {
    name: 'Event Type',
    selector: 'eventType',

    maxWidth: '600px', // when using custom you should use width or maxWidth, otherwise, the table will default to flex grow behavior
    cell: row => <CustomEvent row={row.eventType} />,
  },
  {
    name: 'Location',
    selector: 'eventLocation',
    maxWidth: '600px', // when using custom you should use width or maxWidth, otherwise, the table will default to flex grow behavior
    cell: row => <CustomEvent row={row.eventLocation} />,

  }

];

function p1() {
  return(<p>test</p>);
}

class CustomeDataTable extends React.Component {
  render() {
    return ( <DataTable
        title='Current Active Events'
        columns={columns}
        data={data}
      />
    );
  }
}

export default CustomeDataTable;