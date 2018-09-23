import React from 'react';
import { SetView } from './SetView'

class Home extends React.Component {
  render() {
    return (
      <div>
        <SetView set={'core'} />
      </div>
    );
  }
}

export { Home };
