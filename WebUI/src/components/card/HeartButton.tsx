import { useState } from 'react';
import styled from 'styled-components';
import { AiOutlineHeart, AiFillHeart } from 'react-icons/ai';

const FilledHeart = styled(AiFillHeart)`
  display: block;
  height: 16px;
  width: 16px;
  color: #ea4c89;
`;

const OutlinedHeart = styled(AiOutlineHeart)`
  display: block;
  height: 16px;
  width: 16px;
  color: #9e9ea7;
`;

const HeartButton = () => {
  const [isHovered, setIsHovered] = useState(false);

  const Heart = () => {
    return isHovered ? <FilledHeart /> : <OutlinedHeart />;
  };

  return (
    <div onMouseEnter={() => setIsHovered(true)} onMouseLeave={() => setIsHovered(false)}>
      <Heart />
    </div>
  );
};

export default HeartButton;
