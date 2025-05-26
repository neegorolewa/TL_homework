import { useState, type FormEvent } from "react";
import type { FeedbackData } from "../../types/FeedbackData";

interface FeedbackFormProps {
    onSubmit: (data: FeedbackData) => void;
}

export default function FeedbackForm({ onSubmit }: FeedbackFormProps) {
    const [name, setName] = useState('');
    const [message, setMessage] = useState('');

    const handleSubmit = (event: FormEvent) => {
        event.preventDefault();
        if (!name.trim() || !message.trim()) return;

        
    }
}