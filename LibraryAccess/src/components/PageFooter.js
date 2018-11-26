import React from 'react';
import { Link, Redirect } from 'react-router-dom';


class PageFooter extends React.Component {
  render() { return (
    <div className={'row h-100 justify-content-center align-items-center footerBorder footer pb-3'}>
      <div className={'col-3'} />
      <div className={'col-6 mb-2'}>
      <div className='card-group'>
        <div className='card footerCard'>
            <div className='card-body'>
              <h5 className='card-title underline'>Contact Us</h5>
              <ul className={'ulNoStyle'}>
              <li className={'liNoIndent'}>
                <a href={'mailto:contact@libraryaccess.com'} style={{ textDecoration: 'none', color: 'white'}}>Email</a>
              </li>
              </ul>
            </div>
        </div>
        <div className='card footerCard'>
          <div className='card-body'>
            <h5 className='card-title underline'>Support Us</h5>
            <ul className={'ulNoStyle'}>
              <li className={'liNoIndent'}>
                <a href={'http://patreon.com/libraryaccess'} style={{ textDecoration: 'none', color: 'white'}} target={'_blank'}>Patreon</a>
              </li>
            </ul>
          </div>
        </div>
      </div>
        The literal and graphical information presented on this site about KeyForge, including card images, house symbols, and text, is copyright Fantasy Flight Games. This website is not produced by, endorsed by, supported by, or affiliated with Fantasy Flight Games.
      </div>
      <div className={'col-3'}/>
    </div>
  )}
}

export {PageFooter}
