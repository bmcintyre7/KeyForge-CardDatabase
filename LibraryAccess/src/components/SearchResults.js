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
        { display }
      </div>
    );
  }
}

export { SearchResults };
