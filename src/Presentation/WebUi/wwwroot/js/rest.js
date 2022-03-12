let rest = {};
rest.get = async function (url, onSuccess, onFailed) {
    let response = await fetch(url);
    let result = response.json();
    if (response.ok) {
        isSuccess = true;
        onSuccess(result);
    }
    else {
        isSuccess = false;
        onFailed(result);
    }
}