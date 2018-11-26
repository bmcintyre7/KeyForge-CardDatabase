import React from 'react';
import { Link, Redirect } from 'react-router-dom';


class PageHeader extends React.Component {
  render() {
    var results = this.props.numResults;
    return (
      <nav className='navbar topHeader'>
        <Link to={'/'} style={{ textDecoration: 'none', color: 'white'}}>
          <i className='fas fa-book-open fa-lg mr-2'> </i><span className='navbar-brand mb-0 h1'>Library Access</span>
        </Link>
        {results != null && <span className={'float-right'}>Found {results} results!</span>}
      </nav>
  )}
}

export {PageHeader}
