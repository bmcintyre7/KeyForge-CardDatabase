import React from 'react';

import {Link} from 'react-router-dom';
import {CardView} from 'components/CardView';

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

class SearchResults extends React.Component {
  constructor(props) {
    super(props)
    this.getImageString = this.getImageString.bind(this)
    this.httpGetCards = this.httpGetCards.bind(this);
  }

  getImageString(houseName) {
    return '/images/houses/' + houseName + '.png';
  }

  httpGetCards() {
    var theUrl = 'http://localhost:7230/cards' + this.props.location.state.query;
    var xmlHttp = createCORSRequest('GET', theUrl);
    xmlHttp.send(null);
    return xmlHttp.responseText;
  }

  render() {
    var searchResults = JSON.parse(this.httpGetCards());

    console.log(searchResults);
    console.log(searchResults.length);
    var display = new Array();
    for (var i = 0; i < searchResults.length; i++) {
      display.push((
        <div key={'card-' + searchResults[i]['name']} className='displayInline mx-3 my-3'><CardView card={searchResults[i]}/></div>
      ));
    }

    return (
      <div>
        <div className='row h-100 justify-content-center align-items-center'>
          <div className='col-3'/>
          <div className='col-6 text-center mb-5'>
            <img src={'images/banner/labanner.png'} width={'75%'}/>
          </div>
          <div className='col-3'/>
        </div>
        <div className='row h-100 justify-content-center align-items-center'>
          <div className='col-2'/>
          <div className='col-8 text-center'>
            { display }
          </div>
          <div className='col-2'/>
        </div>
      </div>
    );
  }
}

export { SearchResults };
