import React from 'react';

import {Link} from 'react-router-dom';
import {DropdownButton, MenuItem} from 'react-bootstrap'

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

class AdvancedSearch extends React.Component {
  constructor(props) {
    super(props)
    this.getImageString = this.getImageString.bind(this)
    this.httpGetHouses = this.httpGetHouses.bind(this);
    this.makeTextSearchField = this.makeTextSearchField.bind(this);
    this.makeComparisonSearchField = this.makeComparisonSearchField.bind(this);
    this.makeChecklistSearchField = this.makeChecklistSearchField.bind(this);
    this.slugify = this.slugify.bind(this);
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

  makeTextSearchField(label) {
    let slugifiedLabel = this.slugify(label);
    let testData = new Array(
      { id: '1', name: 'Common'},
      { id: '2', name: 'Uncommon'},
      { id: '3', name: 'Rare'}
    );

    return (
      <div className='row h-100 justify-content-center align-items-center'>
      <div className='col-2' />
      <div className='col-2'>
        <div className='searchLabel float-right'>
          {label}:
        </div>
      </div>
      <div className='col-6 text-center displayInline mx-0 px-0'>
        <div className='px-0 mx-0 minWidth-150'>
          <input type='text typeahead' data-provide='testData' className='form-control' id={slugifiedLabel+'_Search'} />
        </div>
      </div>
      <div className='col-2' />
    </div>
    );
  }

  slugify(text)
  {
    return text.toString().toLowerCase()
      .replace(/\s+/g, '-')           // Replace spaces with -
      .replace(/[^\w\-]+/g, '')       // Remove all non-word chars
      .replace(/\-\-+/g, '-')         // Replace multiple - with single -
      .replace(/^-+/, '')             // Trim - from start of text
      .replace(/-+$/, '');            // Trim - from end of text
  }

  makeComparisonSearchField(label) {
    let slugifiedLabel = this.slugify(label);
    return (
      <div className='row h-100 justify-content-center align-items-center'>
        <div className='col-2' />
        <div className='col-2'>
          <div className='searchLabel float-right'>
            {label}:
          </div>
        </div>
        <div className='col-6 text-center mx-0 px-0'>
          <div className='px-0 mx-0 minWidth-150'>
            <div className='row'>
              <div className='col-4'>
                <select id={slugifiedLabel+'_Select'} className='custom-select form-control mt-0'>
                  <option defaultValue value='='>{'='}</option>
                  <option value=">=">{'>='}</option>
                  <option value="<=">{'<='}</option>
                  <option value=">">{'>'}</option>
                  <option value="<">{'<'}</option>
                </select>
              </div>
              <div className='col-8 mx-0 my-0 py-0'>
                <input type='text' className='form-control' id={slugifiedLabel+'_Value'} />
              </div>
            </div>
          </div>
        </div>
        <div className='col-2' />
      </div>
    );
  }

  makeChecklistSearchField(label, options, disclaimer) {
    let slugifiedLabel = this.slugify(label);
    var display = new Array();
    for (var i = 0; i < options.length; ++i)
      display.push((
        //<Link to={'/cards/house/' + houses[i]}>
        <div key={'checkbox-'+slugifiedLabel+'-'+i}className='col-auto displayInline px-0 mx-3'>
          <label className='align-middle m-0 p-0'><input className='align-middle mr-1' type='checkbox' value=''/>{options[i]}</label>
        </div>
        //</Link>
      ));

    return (
      <div className='row h-100 justify-content-center align-items-center'>
      <div className='col-2' />
      <div className='col-2'>
        <div className='searchLabel float-right'>
          {label}:
        </div>
      </div>
      <div className='col-6 text-center displayInline mx-0 px-0'>
        <div className='px-0 mx-0 py-2  rounded minWidth-150' >
          <div className={'row h-100 mx-0'}>
            {display}
            <br/>
            <span className='tinyLabel float-left mx-3'>{disclaimer}</span>
          </div>
        </div>
      </div>
      <div className='col-2' />
    </div>
    );
  }

  render() {
    var houses = this.httpGetHouses().split(', ');
    var displayHouses = new Array();
    for (var i = 0; i < houses.length; ++i)
      displayHouses.push((
        //<Link to={'/cards/house/' + houses[i]}>
        <div key={'houses-'+i}className='displayInline maxWidth-100'>
          <input type='checkbox' value=''/><img className='mx-1 smallHouseLogo' src={this.getImageString(houses[i])}/>
        </div>
        //</Link>
      ));

    return (
      <div className='justify-content-center align-items-center text-center'>
        <div className='fluid-container h-100 m-5 displayInline px-4 py-2'>
          {this.makeTextSearchField('Name')}
          <br/>
          <div className='row h-100 justify-content-center align-items-center'>
            <div className='col-2' />
            <div className='col-2'>
                <div className='searchLabel float-right'>
                  Houses:
                </div>
            </div>
            <div className='col-6 text-center displayInline mx-0 px-0'>
              <div className='px-0 mx-0 py-2 rounded minWidth-150' >
                {displayHouses}
                <br/>
                <span className='tinyLabel float-left mx-3'>{'Select each house that the cards must be.'}</span>
              </div>
            </div>
            <div className='col-2' />
          </div>
          <br/>
          {this.makeTextSearchField('Card Type')}
          <br/>
          {this.makeTextSearchField('Text')}
          <br/>
          {this.makeComparisonSearchField('Aember')}
          <br/>
          {this.makeComparisonSearchField('Power')}
          <br/>
          {this.makeComparisonSearchField('Armor')}
          <br/>
          {this.makeTextSearchField('Keywords')}
          <br/>
          {this.makeTextSearchField('Traits')}
          <br/>
          {this.makeChecklistSearchField('Rarity', ['Common', 'Uncommon', 'Rare'], 'Select each rarity that the cards must be.')}
          <br/>
          {this.makeTextSearchField('Artist')}
          <br/>
          <div className='row  justify-content-center align-items-center'>
            <div className='col-3' />
            <div className='col'>
              <button className='btn'>Search</button>
            </div>
            <div className='col-2' />
          </div>
        </div>
      </div>
    );
  }
}

export { AdvancedSearch };
