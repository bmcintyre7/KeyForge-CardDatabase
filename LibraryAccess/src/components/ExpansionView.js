import React from 'react';
import { CardView } from './CardView'

class ExpansionView extends React.Component {
  render() {

    var display = new Array();

    //var data = require('../data/' + this.props.set + '.json'); // forward slashes will depend on the file location

    //console.log(JSON.stringify(data));
    //var setId = window.location.href;
    //setId = setId.substring(setId.lastIndexOf('/') + 1, setId.length);
    for(var i = 1; i < 368; i++) {
      display.push((
        <div key={'card-' + i} className='displayInline mx-3 my-3'><CardView expansion={this.props.match.params.expansionName} number={i}/></div>
      ));
    }

    return (
      <div>
        { display }
      </div>
    );
  }
}

export { ExpansionView }
