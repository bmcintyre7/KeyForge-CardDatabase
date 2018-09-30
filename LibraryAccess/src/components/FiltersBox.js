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

class FiltersBox extends React.Component {
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
    var houses = this.httpGetHouses().split(', ');
    var display = new Array();
    for(var i = 0; i < houses.length; ++i)
      display.push((
        // TODO: Figure out filters? This should filter by house. Maybe get all cards in the beginning? Seems awful.
        <Link to={'/cards/house/' + houses[i]}>
          <img className='mx-1' src={this.getImageString(houses[i])} />
        </Link>
      ));

    return (
      <div className='justify-content-center align-items-center text-center'>
        <div className='fluid-container h-100 border border-3 m-5 displayInline px-4 py-2'>
          <div className="row h-100 justify-content-center align-items-center">
            <div className="col-12 px-0 text-center">
              {display}
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export { FiltersBox };
