# Create a new release

echo 'Updating Sentry Project: finpe'
echo 'New Release: '${DRONE_BRANCH}':'${DRONE_COMMIT:0:7}

curl https://sentry.io/api/0/organizations/finpe/releases/ \
  -X POST \
  -H 'Authorization: Bearer '${SENTRY_AUTH_TOKEN} \
  -H 'Content-Type: application/json' \
  -d '
  {
    "version": "'${DRONE_COMMIT:0:7}'",
    "refs": [{
        "repository":"tiagodeliberali/finpe",
        "commit":"'${DRONE_COMMIT_SHA}'"
    }],
    "projects":["finpe-api", "finpe-front"]
}
'
