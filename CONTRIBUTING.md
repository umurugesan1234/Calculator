Contributing to Calculator

Thank you for helping improve this project — this short guide explains the branch workflow and basic rules for contributions.

Principles
- Development work happens on the `dev` branch. Do not push feature work directly to `main`.
- Keep changes small and focused. Open a single PR per logical change.
- Run the build locally before opening a PR: `dotnet build`.

Branch & PR workflow
1. Sync and start from `dev`:
   - `git checkout dev`
   - `git pull origin dev`
2. Create a feature branch off `dev`:
   - `git checkout -b feat/short-description`
3. Work, build, and verify locally:
   - `dotnet build`
   - (Optionally) run any tests if added later.
4. Commit messages
   - Use short, descriptive messages. Optionally follow Conventional Commits: `feat:`, `fix:`, `chore:`.
5. Push the branch and open a Pull Request targeting `main` when the change is ready. Describe the change, link issues, and include screenshots if UI changed.
6. PR review & merge
   - At least one reviewer approval is required (project policy). Maintainers may request changes.
   - Use squash or merge commits depending on project preference (squash recommended for small projects).

Coding & formatting
- Keep code readable and small.
- Use your IDE's formatter (Visual Studio / Rider) and follow C# conventions.

CI and checks
- PRs should pass the project build and any CI checks before being merged.
- If you add tests or other checks, update the PR description to highlight them.

Reporting issues
- Open issues for bugs or feature requests. Provide steps to reproduce and expected behavior.

Thank you — contributions are welcome!
