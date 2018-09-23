import React from 'react';
import { CardView } from './CardView'

class SetView extends React.Component {
  render() {

    var display = new Array();

    var data = require('../data/' + this.props.set + '.json'); // forward slashes will depend on the file location

    console.log(JSON.stringify(data));
    for(var i = 0; i < data.length; i++) {
      display.push((
        <div key={'card-' + i} className='displayInline mx-3 my-3'><CardView card={data[i]}/></div>
      ));
    }

    return (
      <div>
        { display }
      </div>
    );
  }
}

export { SetView }
