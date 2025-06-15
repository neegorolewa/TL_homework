import AngryFace from '../../../public/angry-face.svg';
import SadFace from '../../../public/sad-face.svg';
import NeutralFace from '../../../public/neutral-face.svg';
import SmilingFace from '../../../public/smiling-face.svg';
import BigEyesFace from '../../../public/big-eyes-face.svg';
import type { EmojiButtonList } from '../../data/data.ts';
import classes from './EmojiButtonList.module.css';
import EmojiButton from '../EmojiButton/EmojiButton';

interface EmojiButtonListProps {
    selectedRating: number;
    onSelectRating: (rating: number) => void;
}

const buttonsList: EmojiButtonList[] = [
    { rate: 1, emoji: { src: AngryFace, alt: 'AngryFace' } },
    { rate: 2, emoji: { src: SadFace, alt: 'SadFace' } },
    { rate: 3, emoji: { src: NeutralFace, alt: 'NeutralFace' } },
    { rate: 4, emoji: { src: SmilingFace, alt: 'SmilingFace' } },
    { rate: 5, emoji: { src: BigEyesFace, alt: 'BigEyesFace' } },
]

export default function EmojiButtonList({selectedRating, onSelectRating} : EmojiButtonListProps) {
    
    function handleClick(rate: number) {
        const currentRating = rate === selectedRating ? 0 : rate;
        onSelectRating(currentRating);
    }

    return (
        <ul className={classes.container}>
            {buttonsList.map((button) => 
                <EmojiButton 
                    key={button.rate}
                    emojiData={button.emoji}
                    isActive={button.rate === selectedRating}
                    onClick={() => handleClick(button.rate)}
                />
            )}
        </ul>
    )
}