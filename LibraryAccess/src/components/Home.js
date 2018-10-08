import React from 'react';

import { Link } from 'react-router-dom'
import { SearchBox } from './SearchBox'


class Home extends React.Component {
  render() {
    return (
      <div className='justify-content-center align-items-center text-center'>
        <div className='fluid-container h-100 m-5 displayInline px-4 py-2'>
          <div className='row h-100 justify-content-center align-items-center h1'>
            <p>Welcome to Library Access!</p>
          </div>
          <div className='row h-100'>
            <div className='col-2' />
            <div className='col-8'>
              <SearchBox />
            </div>
            <div className='col-2' />
          </div>
        </div>
      </div>
    );
  }
}

export { Home };
