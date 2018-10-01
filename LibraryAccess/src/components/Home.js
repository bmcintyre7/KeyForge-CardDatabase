import React from 'react';

import { Link } from 'react-router-dom'
import { SearchBox } from './SearchBox'


class Home extends React.Component {
  render() {
    return (
      <div>
        Welcome to Library Access!
        <br/>
        <SearchBox />
      </div>
    );
  }
}

export { Home };
