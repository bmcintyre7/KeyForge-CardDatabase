import React from 'react';

import {Link} from 'react-router-dom';
import {apiURL, createCORSRequest} from 'sources/createCORSRequest';

class SearchBox extends React.Component {
  constructor(props) {
    super(props)
    this.getImageString = this.getImageString.bind(this)
    this.httpGetHouses = this.httpGetHouses.bind(this);
  }

  getImageString(houseName) {
    return '/images/houses/' + houseName + '.png';
  }

  httpGetHouses() {
    var theUrl = apiURL + '/houses';
    var xmlHttp = createCORSRequest('GET', theUrl)
    xmlHttp.send(null);
    return xmlHttp.responseText;
  }

  render() {
    return (
      <div>
        <div className='input-group'>
          <input type='text' className='form-control' id='search' />
          <div className='input-group-append'>
            <button className="btn btn-default" type="button">Search</button>
          </div>
        </div>
        <br/>
        <Link to={'/advanced'}>
          Advanced Search
        </Link>
      </div>
    );
  }
}

export { SearchBox };
