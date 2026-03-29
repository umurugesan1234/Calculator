Branch protection recommendations for GitHub

Purpose
- Protect `main` from direct pushes and require a review/CI before merging.
- Keep `dev` open for active development but also require at least minimal review and green build to avoid broken main merges.

Recommended settings (Main)
- Protect the `main` branch.
- Require pull requests before merging (disallow direct pushes).
- Require at least 1 approving review before merge (2 for stricter projects).
- Require status checks to pass (set the CI build/check name, e.g. `build` or your CI workflow name).
- Require branches to be up-to-date before merging (optional, enforces rebasing or merging main into branch).
- Enforce linear history (optional, prevents merge commits if desired).
- Restrict who can push to the branch (admins only) — optional.

Recommended settings (Dev)
- Protect the `dev` branch with lighter rules to avoid accidental destructive pushes:
  - Require PRs to merge into `dev` (optional for team workflow).
  - Require at least 1 approval.
  - Require passing build/status checks if CI exists.

How to enable
- Use the GitHub repository Settings → Branches → Branch protection rules to configure the above.
- For automation, use the GitHub REST API `PUT /repos/{owner}/{repo}/branches/{branch}/protection`.

Suggested CI check name
- If you add a GitHub Actions workflow, name the build job `build` (or similar) and add it to required status checks.

Notes
- Enabling "Require status checks to pass" requires that a GitHub Actions workflow or external CI reports a status check with the chosen name.
- If you want, I can add a minimal GitHub Actions workflow that runs `dotnet build` on PRs and push that as `.github/workflows/ci.yml` so you can make `build` required.

If you'd like, I will create the minimal `ci.yml` GitHub Actions workflow next and update the README to reference the required checks.
