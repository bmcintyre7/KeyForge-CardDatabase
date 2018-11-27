import React from 'react';

export const ICONS = {
  ALIGN_JUSTIFY: 'align-justify',
  DUMBBELL: 'dumbbell',
  FINGERPRINT: 'fingerprint',
  GEM: 'gem',
  HOME: 'home',
  ID_CARD: 'id-card',
  KEY: 'key',
  LIST_UI: 'list-ui',
  PAINT_BRUSH: 'paint-brush',
  SHIELD_ALT: 'shield-alt',
  STAR: 'star'
}


const IconTitle = ({ faIcon, solid = false, title }) => (
  <span className={`${solid ? 'fas' : 'far'} fa-${faIcon} fa-sm`}>
    <div className='searchLabel displayInline pl-2'>
      {title}:
    </div>
  </span>
)

export default IconTitle