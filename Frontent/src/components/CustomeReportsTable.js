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
      imgData: this.props.imgData,
      renderedOutput:this.props.imgData.map((item,i) => <img key={i} src={`data:image/png;base64,${item.reportContent}`}
             style={{height:500+"px", width:300+"px"}}/>),

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

  }


  render() {
    return (
      <div>

      <DataTable
        title='Reports About The Incident'
        columns={this.state.columns}
        data={this.state.data}/>
        <div id="images">
        <div>
        {/*Displaying Images of the reporting*/}
          {this.state.renderedOutput}
        </div>
        </div>
      </div>
    );
  }
}

export default CustomeReportsTable;
