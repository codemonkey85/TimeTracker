export function initialize() {
    let timeTrackerIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
    timeTrackerIndexedDb.onupgradeneeded = function () {
        let db = timeTrackerIndexedDb.result;
        db.createObjectStore("TimeEntries", { keyPath: "id" });
        db.createObjectStore("WeekEntries", { keyPath: "id" });
    }
}

let CURRENT_VERSION = 1;
let DATABASE_NAME = "TimeTracker";

export async function get(storeName, id) {
    let request = new Promise((resolve) => {
        let timeTrackerIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
        timeTrackerIndexedDb.onsuccess = function () {
            let transaction = timeTrackerIndexedDb.result.transaction(storeName, "readonly");
            let collection = transaction.objectStore(storeName);
            let result = collection.get(id);
            result.onsuccess = function (e) {
                resolve(result.result);
            }
        }
    });
    let result = await request;
    return result;
}

export async function getAll(storeName) {
    let request = new Promise((resolve) => {
        let timeTrackerIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
        timeTrackerIndexedDb.onsuccess = function () {
            let transaction = timeTrackerIndexedDb.result.transaction(storeName, "readonly");
            let collection = transaction.objectStore(storeName);
            let result = collection.getAll();
            result.onsuccess = function (e) {
                resolve(result.result);
            }
        }
    });
    let result = await request;
    return result;
}

export function set(storeName, value) {
    let timeTrackerIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
    timeTrackerIndexedDb.onsuccess = function () {
        let transaction = timeTrackerIndexedDb.result.transaction(storeName, "readwrite");
        let collection = transaction.objectStore(storeName)
        collection.put(value);
    }
}

export async function remove(storeName, id) {
    let request = new Promise((resolve) => {
        let timeTrackerIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
        timeTrackerIndexedDb.onsuccess = function () {
            let transaction = timeTrackerIndexedDb.result.transaction(storeName, "readwrite");
            let collection = transaction.objectStore(storeName);
            let result = collection.delete(id);
            result.onsuccess = function (e) {
                resolve(result.result);
            }
        }
    });
    let result = await request;
    return result;
}
