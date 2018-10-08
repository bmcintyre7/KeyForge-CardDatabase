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

class CardView extends React.Component {
  constructor(props) {
    super(props)
    this.getImageString = this.getImageString.bind(this);
    this.httpGetCard = this.httpGetCard.bind(this);
  }

  getImageString(fileName) {
    return '/images/cards/' + fileName + '.jpg';
  }

  httpGetCard() {
    var theUrl = 'http://localhost:7230/cards/' + this.props.card;
    var xmlHttp = createCORSRequest('GET', theUrl)
    //xmlHttp.open('GET', theUrl, false); // false for synchronous request
    xmlHttp.send(null);
    return xmlHttp.responseText;
  }

  render() {
    //console.log(this.httpGetCard())
    //var theCard = JSON.parse(this.httpGetCard())
    var theCard = this.props.card;
    var fullExpansionObj = theCard['expansions'][theCard['expansions'].length - 1]
    var newestExpansion = fullExpansionObj.substr(0, fullExpansionObj.indexOf(' #'));
    var newestImage = theCard['imageNames'][theCard['imageNames'].length - 1];
    var newestExpansionNumber = fullExpansionObj.substr(fullExpansionObj.indexOf(' #') + 2);

    return (
      <div>
        {theCard.name}
        <br/>
        {theCard['expansions'][theCard['expansions'].length - 1]}
        <br/>
        <Link to={'/cards/' + newestExpansion + '/' + newestExpansionNumber}>
          <img src={ this.getImageString(newestImage) } alt={ 'test' } width='250' height='350'/>
        </Link>
      </div>
    );
  }
}

export { CardView };
