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
      <div className='justify-content-center align-items-center text-center'>
        <div className='container h-100 displayInline'>
          <div className='row'>
            <div className='col-sm' />
            <div className='col-sm'>
              <div className='form-group'>
                <label for='search'>Search:</label>
                <input type='text' className='form-control' id='search' />
                <Link to={'/advanced'}>
                  Advanced Search
                </Link>
               </div>
            </div>
            <div className='col-sm' />
          </div>
        </div>
      </div>
    );
  }
}

export { SearchBox };
