# Branch protection recommendations for GitHub

## Purpose
- Protect `main` from direct pushes and require a review/CI before merging.
- Keep `dev` open for active development but require at least a minimal review and a green build to avoid broken merges into `main`.

## Recommended settings (main)
- Protect the `main` branch.
- Require pull requests before merging (disallow direct pushes).
- Require at least 1 approving review before merge (2 for stricter projects).
- Require status checks to pass (set the CI build/check name, e.g. `build` or your CI workflow name).
- Optionally require branches to be up-to-date before merging (enforces rebasing or merging `main` into the branch).
- Optionally enforce linear history (prevents merge commits if desired).
- Optionally restrict who can push to the branch (admins only).

## Recommended settings (dev)
- Protect the `dev` branch with lighter rules to avoid accidental destructive pushes:
  - Optionally require PRs to merge into `dev` (depends on team workflow).
  - Require at least 1 approval for changes that affect shared behavior.
  - Require passing build/status checks if CI exists.

## How to enable
- Use the GitHub repository Settings → Branches → Branch protection rules to configure the above.
- For automation, use the GitHub REST API `PUT /repos/{owner}/{repo}/branches/{branch}/protection`.

### Suggested CI check name
- If you add a GitHub Actions workflow, name the build job `build` (or similar) and add it to required status checks.

## Notes
- Enabling "Require status checks to pass" requires that a GitHub Actions workflow or external CI reports a status check with the chosen name.
- If you want, I can add a minimal GitHub Actions workflow that runs `dotnet build` on PRs and push that as `.github/workflows/ci.yml` so you can make `build` required.

If you'd like, I will create the minimal `ci.yml` GitHub Actions workflow next and update the README to reference the required checks.
