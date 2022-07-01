let manage = {}

manage.user = {}
manage.user.init = function () {
    rest.get('https://localhost:5001/manage/user/getAll',
        function onSuccess() {
            alert('success');
        },
        function onFailed() {
            alert('failed');
        });
}