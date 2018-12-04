import React from 'react';
import MetaTags from 'react-meta-tags'
import {apiURL} from 'shared/createCORSRequest'
import {PageHeader} from "components/PageHeader";
import {PageFooter} from "components/PageFooter";
import axios from "axios";

class DetailCardView extends React.Component {
  state = {
    theCard: [],
  }

  constructor(props) {
    super(props);
    this.getImageString = this.getImageString.bind(this);
    this.httpGetCard = this.httpGetCard.bind(this);
    this.toggleForm = this.toggleForm.bind(this);
    this.submitForm = this.submitForm.bind(this);
  }

  componentWillMount() {
    this.httpGetCard(this.props.match.params.expansionName, this.props.match.params.cardId)
  }

  componentDidMount() {
    window.scrollTo(0,0);
  }

  getImageString(expansion, number) {
    return '/images/cards/' + expansion.toLowerCase() + '-' + number + '.jpg';
  }

  httpGetCard(expansion, cardId) {
    var theUrl = apiURL + '/cards/' + expansion + '/' + cardId;
    axios.get(theUrl).then(newData => {
      this.setState({theCard: newData.data});
    });
    //let theUrl = apiURL + '/cards/' + expansion + '/' + cardId;
    //let xmlHttp = createCORSRequest('GET', theUrl);
    //xmlHttp.send(null);
    //return xmlHttp.responseText;
  }

  toggleForm() {
    $("#form1").toggle();
  }

  submitForm() {
      Email.send("bmcintyre@entrick.net",
        "bill@libraryaccess.net",
        "Error Report + " + window.location.href,
        "Error Description: " + $('#errorDescription').val(),
        {token:"15a3332b-48a1-4f31-9a1a-252beeafbd33"});
      this.toggleForm();
  }

  render() {
    let cardId = this.props.match.params.cardId;
    let expansionId = this.props.match.params.expansionName;
    //let result = this.httpGetCard(expansionId, cardId);
    //let theCard = JSON.parse(result);

    document.getElementById('og-image').setAttribute('content', this.getImageString(expansionId, cardId));
    document.getElementById('og-image').content = this.getImageString(expansionId, cardId);

    var rebuiltText = new Array();
    if (this.state.theCard.text != null) {
      var parts = this.state.theCard.text.split('[AE]');
      console.log("P: " + parts.length);
      for (var i = 0; i < parts.length; ++i) {
        var damageParts = parts[i].split('[D]');
        console.log("D: " + damageParts.length);
        if (damageParts.length > 1)
          for (var j = 0; j < damageParts.length; ++j) {
            rebuiltText.push(damageParts[j]);
            if (j != damageParts.length - 1)
              rebuiltText.push(<img src={'/images/icons/damage.svg'} className={'iconPadBottom'} width={'16'} height={'16'}/>)
          }
        if (damageParts.length == 1)
          rebuiltText.push(parts[i]);
        if (i != parts.length - 1)
          rebuiltText.push(<img src={'/images/icons/aember.png'} className={'iconPadBottom'} width={'16'} height={'16'}/>);
      }
    }

    return (
      <div>
        <MetaTags>
        <meta id={'og-image'} property={'og:image'} name={'og:image'} content={this.getImageString(expansionId, cardId)} />
        </MetaTags>
        <PageHeader/>
        <div className='row h-100 justify-content-center align-items-center'>
          <div className='col-3'/>
          <div className='col-6 text-center mb-5 minWidth-400'>
            <img className={'displayInline imageBorder mr-2 mb-5'} src={ this.getImageString(expansionId, cardId) } alt={ 'test' } width='250' height='350'/>
              <div className={'displayInline mx-3'}>
                {this.state.theCard.name != null &&
                <div className={'text-left'}><b>Name:</b> {this.state.theCard.name}</div>}
                {this.state.theCard.houses != null &&
                <div className={'text-left'}><b>House:</b> {this.state.theCard.houses[0]}</div>}
                {this.state.theCard.aember != null &&
                <div className={'text-left'}><b>Aember:</b> {this.state.theCard.aember}</div>}
                {this.state.theCard.power != null &&
                <div className={'text-left'}><b>Power:</b> {this.state.theCard.power}</div>}
                {this.state.theCard.armor != null &&
                <div className={'text-left'}><b>Armor:</b> {this.state.theCard.armor}</div>}
                {this.state.theCard.text != null &&
                <div className={'text-left maxCardText'}><b>Text:</b> {rebuiltText}</div>}
                {this.state.theCard.rarity != null &&
                <div className={'text-left'}><b>Rarity:</b> {this.state.theCard.rarity}</div>}
                {this.state.theCard.artist != null &&
                <div className={'text-left'}><b>Artist:</b> {this.state.theCard.artist}</div>}
              </div>
          </div>
          <div className={'col-3'} />
          <div className='row h-100 justify-content-center align-items-center'>
            <div className={'col-12'}>
              <button type='button' id='formButton' className={'form-control btn'} onClick={this.toggleForm}>Report an error with this card</button>
              <form id='form1' className={'mt-3'}>
                <b>Error Description:</b> <textarea id='errorDescription' className={'form-control'} name='firstName'></textarea>
                <br/>
                <button type='button' className={'form-control btn'} id='submit' onClick={this.submitForm}>Submit</button>
              </form>
            </div>
          </div>
        </div>
        <PageFooter/>
      </div>
    );
  }
}



export { DetailCardView };
