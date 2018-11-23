import React from 'react';
import {apiURL} from './Home'
import {PageHeader} from "components/PageHeader";

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

class DetailCardView extends React.Component {
  constructor(props) {
    super(props);
    this.getImageString = this.getImageString.bind(this);
    this.httpGetCard = this.httpGetCard.bind(this);
    this.toggleForm = this.toggleForm.bind(this);
    this.submitForm = this.submitForm.bind(this);
  }

  getImageString(expansion, number) {
    return '/images/cards/' + expansion + '-' + number + '.jpg';
  }

  httpGetCard(expansion, cardId) {
    let theUrl = apiURL + '/cards/' + expansion + '/' + cardId;
    let xmlHttp = createCORSRequest('GET', theUrl);
    xmlHttp.send(null);
    return xmlHttp.responseText;
  }

  toggleForm() {
    $("#form1").toggle();
  }

  submitForm() {
      Email.send("bmcintyre@entrick.com",
        "riek.account@gmail.com",
        "Error Report + " + window.location.href,
        "Error Description: " + $('#errorDescription').val(),
        {token:"15a3332b-48a1-4f31-9a1a-252beeafbd33"});
      this.toggleForm();
  }

  render() {
    let cardId = this.props.match.params.cardId;
    let expansionId = this.props.match.params.expansionName;
    let result = this.httpGetCard(expansionId, cardId);
    let theCard = JSON.parse(result);
    return (
      <div>
        <PageHeader/>
        <div className='row h-100 justify-content-center align-items-center'>
          <div className='col-3'/>
          <div className='col-6 text-center mb-5 minWidth-400'>
            <img className={'displayInline imageBorder mr-2 mb-5'} src={ this.getImageString(expansionId, cardId) } alt={ 'test' } width='250' height='350'/>
              <div className={'displayInline mx-3'}>
                <div className={'text-left'}><b>Name:</b> {theCard.name}</div>
                <div className={'text-left'}><b>House:</b> {theCard.houses[0]}</div>
                {theCard.aember != null &&
                <div className={'text-left'}><b>Aember:</b> {theCard.aember}</div>}
                {theCard.power != null &&
                <div className={'text-left'}><b>Power:</b> {theCard.power}</div>}
                {theCard.armor != null &&
                <div className={'text-left'}><b>Armor:</b> {theCard.armor}</div>}
                <div className={'text-left maxCardText'}><b>Text:</b> {theCard.text}</div>
                <div className={'text-left'}><b>Rarity:</b> {theCard.rarity}</div>
                {theCard.artist != null &&
                <div className={'text-left'}><b>Artist:</b> {theCard.artist}</div>}
              </div>
          </div>
          <div className={'col-3'} />
          <div className='row h-100 justify-content-center align-items-center'>
            <div className={'col-12'}>
              <button type='button' id='formButton' className={'form-control'} onClick={this.toggleForm}>Report an error with this card</button>
              <form id='form1' className={'mt-3'}>
                <b>Error Description:</b> <textarea id='errorDescription' className={'form-control'} name='firstName'></textarea>
                <br/>
                <button type='button' className={'form-control'} id='submit' onClick={this.submitForm}>Submit</button>
              </form>
            </div>
          </div>
        </div>
      </div>
    );
  }
}



export { DetailCardView };
