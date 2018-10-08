import React from 'react';

import {Link} from 'react-router-dom';

function createCORSRequest(method, url) {
  var xhr = new XMLHttpRequest();
  if ("withCredentials" in xhr) {
    // XHR for Chrome/Firefox/Opera/Safari.
    xhr.open(method, url, false);
  } else if (typeof XDomainRequest != "undefined") {
    // XDomainRequest for IE.
    xhr = new XDomainRequest();
    xhr.open(method, url);
  } else {
    // CORS not supported.
    xhr = null;
  }
  return xhr;
}

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
    var theUrl = 'http://localhost:7230/houses';
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
