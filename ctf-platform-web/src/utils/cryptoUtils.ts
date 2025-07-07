import CryptoJS from 'crypto-js';

const SECRET_KEY = 'your-secret-key-123'; // 建议使用环境变量或服务端下发

export function encrypt(data: string): string {
    return CryptoJS.AES.encrypt(data, SECRET_KEY).toString();
}

export function decrypt(cipherText: string): string {
    const bytes = CryptoJS.AES.decrypt(cipherText, SECRET_KEY);
    return bytes.toString(CryptoJS.enc.Utf8);
}