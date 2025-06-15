import classes from './EmojiButton.module.css';
import type { EmojiButtonData } from '../../data/data.ts';

interface EmojiButtonProps {
    emojiData: EmojiButtonData;
    isActive: boolean;
    onClick: () => void;
}

export default function EmojiButton({ emojiData, isActive, onClick }: EmojiButtonProps) {
    return (
        <button
            type="button"
            onClick={onClick}
            className={`${classes.button} ${isActive ? `${classes.active}` : ''}`}
        >
            <img src={emojiData.src} alt={emojiData.alt} />
        </button>
    )
}