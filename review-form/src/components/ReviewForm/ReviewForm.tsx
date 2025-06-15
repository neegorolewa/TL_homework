import { useState } from "react";
import HeaderText from "../HeaderText/HeaderText";
import EmojiButtonList from "../EmojiButtonList/EmojiButtonList";
import classes from './ReviewForm.module.css';
import ReviewButton from "../ReviewButton/ReviewButton";
import type { ReviewData } from "../../data/data";

interface ReviewFormProps {
    onSubmit: (data: ReviewData) => void;
}

export default function ReviewForm({onSubmit}: ReviewFormProps) {
    const [rating, setRating] = useState<number>(0);
    const [username, setUsername] = useState<string>('');
    const [description, setDescription] = useState<string>('');

    const isFormValid = username.trim() !== '' && description.trim() !== '' && rating > 0

    const handleSumbit = (event: React.FormEvent) => {
        event.preventDefault();
        if (isFormValid) {
            console.log({ rating, username, description })
            onSubmit({
                username: username.trim(),
                description: description.trim(),
                rating,
            });
        }
        setUsername('');
        setDescription('');
        setRating(0);
    }

    return (
        <form className={classes.form} onSubmit={handleSumbit}>
            <HeaderText />
            <EmojiButtonList
                selectedRating={rating}
                onSelectRating={setRating}
            />
            <div className={classes.usernameContainer}>
                <label htmlFor="username">*Имя</label>
                <input
                    type="text"
                    id="username"
                    placeholder="Как вас зовут?"
                    value={username}
                    onChange={(event) => setUsername(event.target.value)}
                />
            </div>
            <div className={classes.descriptionContainer}>
                <textarea
                    id="description"
                    placeholder="Напишите, что понравилось, что было непонятно"
                    value={description}
                    onChange={(event) => setDescription(event.target.value)}
                ></textarea>
            </div>
            <div className={classes.buttonContainer}>
                <ReviewButton isDisabled={!isFormValid} />
            </div>
        </form>
    )
}