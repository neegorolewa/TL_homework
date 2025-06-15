import classes from './ReviewButton.module.css';

interface ReviewButtonProps {
    isDisabled: boolean;
}

export default function ReviewButton({isDisabled}: ReviewButtonProps) {
    return (
        <button className={classes.button} type='submit' disabled={isDisabled}>
            Отправить
        </button>
    )
}