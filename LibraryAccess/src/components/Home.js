import React from 'react';
import { FiltersBox } from './FiltersBox'

class Home extends React.Component {
  render() {
    return (
      <div>
        Welcome to Library Access!
        <br/>
        <FiltersBox />
      </div>
    );
  }
}

export { Home };
