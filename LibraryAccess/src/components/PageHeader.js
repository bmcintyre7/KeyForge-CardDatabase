import React from 'react';
import { Link, Redirect } from 'react-router-dom';


class PageHeader extends React.Component {
  render() { return (
    <div className={'row h-100 justify-content-center align-items-center'}>
      <div className={'col-3'} />
      <div className={'col-6'} >
        <Link to={'/'} style={{ textDecoration: 'none' }}>
          <span className='topHeader' ><img src={'/images/banner/labanner.png'} className={'banner px-0 mx-0'}/></span>
        </Link>
      </div>
      <div className={'col-3'} />
    </div>
  )}
}

export {PageHeader}
